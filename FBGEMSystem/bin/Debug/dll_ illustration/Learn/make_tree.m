function tree = make_tree(patterns, targets, inc_node, discrete_dim ,maxNbin, base)  %
%         make_tree(train_patterns, train_targets, inc_node, discrete_dim, max(discrete_dim), 0);  
    %Build a tree recursively   递归地构造树
      
    [Ni, L]                     = size(patterns);  %%L为当前的样本总数，Ni为特征数目
    Uc                          = unique(targets);  %训练样本对应的判别标签   无重复的取得这些标签  也就是得到判别标签的个数
    tree.dim                    = 0;  %初始化树的分裂特征为第0个
    %tree.child(1:maxNbin)  = zeros(1,maxNbin);  
    tree.split_loc              = inf;  %初始化分裂位置是inf
      
    if isempty(patterns) 
        return  
    end  
      
    %When to stop: If the dimension is one or the number of examples is small 
    % inc_node为防止过拟合，表示样本数小于一定阈值结束递归，可设置为5-10
    if ((inc_node > L) || (L == 1) || (length(Uc) == 1)) %如果剩余训练样本太小(小于inc_node)，或只剩一个，或只剩一类标签，退出  
        H                   = hist(targets, length(Uc));  %统计样本的标签，分别属于每个标签的数目    H(1×标签数目)
        [m, largest]    = max(H); %得到包含样本数最多的那个标签的索引，记为largest 包含多少个样本，记为m
        tree.Nf         = [];  
        tree.split_loc  = [];  
        tree.child      = Uc(largest);%姑且直接返回其中包含样本数最多一类作为其标签 
        return  
    end  
      
    %Compute the node's I  
    for i = 1:length(Uc) %遍历判别标签的数目 
        Pnode(i) = length(find(targets == Uc(i))) / L;  %得到当前所有样本中 标签=第i个标签 的样本的数目  占样本总数的比例  存放在Pnode(i)中
    end  
%   计算当前的信息熵（分类期望信息）
% 	例如，数据集D包含14个训练样本，9个属于类别“Yes”，5个属于类别“No”，Inode = -9/14 * log2(9/14) - 5/14 * log2(5/14) = 0.940
    Inode = -sum(Pnode.*log(Pnode)/log(2));  
      
    %For each dimension, compute the gain ratio impurity ％对于每维，计算杂质的增益比  对特征集中每个特征分别计算信息熵
    %This is done separately for discrete and continuous patterns     ％对于离散和连续特征，分开计算
    delta_Ib    = zeros(1, Ni);  %Ni是特征维数  用于记录每个特征的信息增益率
    split_loc   = ones(1, Ni)*inf;  %初始化每个特征的分裂值是inf
      
    for i = 1:Ni %遍历每个特征  
        data    = patterns(i,:);  %得到当前所有样本的这个特征的特征值
        Ud      = unique(data);   %得到无重复的特征值构成向量Ud
        Nbins   = length(Ud);     %得到无重复的特征值的数目
        if (discrete_dim(i)) %如果这个特征是离散特征 
            %This is a discrete pattern  
            P   = zeros(length(Uc), Nbins);  %Uc是判别标签的个数   判别标签个数×无重复的特征值的数目
            for j = 1:length(Uc) %遍历每个标签  
                for k = 1:Nbins %遍历每个特征值  
                    indices     = find((targets == Uc(j)) & (patterns(i,:) == Ud(k)));  
                    % &适用于矩阵间的逻辑运算 &&不适用，只能用于单个元素 &的意思也是与
                    %找到 （样本标签==第j个标签  并且 当前所有样本的这个特征==第k个特征值） 的样本个数
                    P(j,k)  = length(indices);  %记为P(j,k)
                end  
            end  
            Pk          = sum(P);  %取P的每一列的和，也就是得到当前所有样本中，这个特征的特征值==这个特征值的样本数目   Pk(1×特征值数目)表示这个特征的特征值等于每个特征值的样本数目
            P1          = repmat(Pk, length(Uc), 1);  %把Pk复制成 判别标签个数 行
            P1          = P1 + eps*(P1==0);  %这主要在保证P1作被除数时不等于0
            P           = P./P1;  %得到当前所有样本中，这个特征的值等于每个特征值且标签等于每个标签的样本，占当前这个特征值中的样本的比例
            Pk          = Pk/sum(Pk);  %得到当前所有样本中，这个特征的值等于每个特征值的样本，占当前样本总数的比例
            info        = sum(-P.*log(eps+P)/log(2));  %对特征集中每个特征分别计算信息熵  info(1×特征值数目)
            delta_Ib(i) = (Inode-sum(Pk.*info))/(-sum(Pk.*log(eps+Pk)/log(2))); %计算得到当前特征的信息增益率
            %计算信息增益率(GainRatio)，公式为Gain(A)/I(A),
            %其中Gain(A)=Inode-sum(Pk.*info)就是属性A的信息增益
            %其中I(A)=-sum(Pk.*log(eps+Pk)/log(2))为属性A的所包含的信息
        else   %对于连续特征(主要要找到合适的分裂值，使数据离散化)
            %This is a continuous pattern  
            P   = zeros(length(Uc), 2);  %   P(判别标签数目×2)  列1代表前..个样本中的标签分布情况   列2代表除前..个样本之外的标签分布情况  这个..就是各个分裂位置
      
            %Sort the patterns  
            [sorted_data, indices] = sort(data);  %data里存放的是当前所有训练样本的这个特征的特征值   从小到大排序  sorted_data是排序好的数据 indices是索引
            sorted_targets = targets(indices);  %当然，判别标签也要随着样本顺序调整而调整
      
            %Calculate the information for each possible split  计算分裂信息度量
              I = zeros(1,Nbins);
              delta_Ib_inter    = zeros(1, Nbins);
              for j = 1:Nbins-1
                  P(:, 1) = hist(sorted_targets(find(sorted_data <= Ud(j))) , Uc);  %记录<=当前特征值的样本的标签的分布情况
                  P(:, 2) = hist(sorted_targets(find(sorted_data > Ud(j))) , Uc);  %记录>当前特征值的样本的标签的分布情况
                  Ps      = sum(P)/L;  %sum(P)是得到分裂位置前面和后面各有样本数占当前样本总数的比例
                  P       = P/L;  %占样本总数的比例
                  Pk      = sum(P);   %%sum(P)是得到分裂位置前面和后面各有多少个样本 比例 
                  P1      = repmat(Pk, length(Uc), 1);  %把Pk复制成 判别标签个数 行
                  P1      = P1 + eps*(P1==0);  
                  info    = sum(-P./P1.*log(eps+P./P1)/log(2));  %计算信息熵（分类期望信息）
                  I(j)    = Inode - sum(Ps.*info);  %Inode-sum(info.*Ps)就是以第j个样本分裂的的信息增益   
                  delta_Ib_inter(j) =  I(j)/(-sum(Ps.*log(eps+Ps)/log(2))); %计算得到当前特征值的信息增益率
              end

            [~, s] = max(I);  %找到信息增益最大的划分方法  delta_Ib(i)中存放的是对于当前第i个特征而言，最大的信息增益作为这个特征的信息增益  s存放这个划分方法
            delta_Ib(i) = delta_Ib_inter(s);  %得到这个分类特征值对应的信息增益率
            split_loc(i) = Ud(s);  %对应特征i的划分位置就是能使信息增益最大的划分值
        end  
    end  
      
    %Find the dimension minimizing delta_Ib    %找到当前要作为分裂特征的特征
    [m, dim]    = max(delta_Ib);  %找到所有特征中最大的信息增益对应的特征，记为dim
    dims        = 1:Ni;  %Ni特征数目
    tree.dim    = dim;  %记为树的分裂特征
      
    %Split along the 'dim' dimension  
    Nf      = unique(patterns(dim,:));  %得到选择的这个作为分裂特征的特征的那一行  也就是得到当前所有样本的这个特征的特征值
    Nbins   = length(Nf);  %得到这个特征的无重复的特征值的数目
    tree.Nf = Nf;  %记为树的分类特征向量 当前所有样本的这个特征的特征值
    tree.split_loc      = split_loc(dim);  %把这个特征的划分位置记为树的分裂位置  可是如果选择的是一个离散特征，split_loc(dim)是初始值inf啊？？？
      
    %If only one value remains for this pattern, one cannot split it.  
    if (Nbins == 1)  %无重复的特征值的数目==1，即这个特征只有这一个特征值，就不能进行分裂
        H               = hist(targets, length(Uc));  %统计当前所有样本的标签，分别属于每个标签的数目    H(1×标签数目)
        [m, largest]    = max(H);  %得到包含样本数最多的那个标签的索引，记为largest 包含多少个样本，记为m
        tree.Nf         = [];  %因为不以这个特征进行分裂，所以Nf和split_loc都为空
        tree.split_loc  = [];  
        tree.child      = Uc(largest);  %姑且将这个特征的标签就记为包含样本数最多的那个标签
        return  
    end  
      
    if (discrete_dim(dim))  %如果当前选择的这个作为分裂特征的特征是个离散特征 
        %Discrete pattern  
        for i = 1:Nbins   %遍历这个特征下无重复的特征值的数目
            indices         = find(patterns(dim, :) == Nf(i));  %找到当前所有样本的这个特征的特征值为Nf(i)的索引们
            tree.child(i)   = make_tree(patterns(dims, indices), targets(indices), inc_node, discrete_dim(dims), maxNbin, base);%递归
            %因为这是个离散特征，所以分叉成Nbins个，分别针对每个特征值里的样本，进行再分叉
        end  
    else  
        %Continuous pattern  %如果当前选择的这个作为分裂特征的特征是个连续特征 
        indices1            = find(patterns(dim,:) <= split_loc(dim));  %找到特征值<=分裂值的样本的索引们
        indices2            = find(patterns(dim,:) > split_loc(dim));   %找到特征值>分裂值的样本的索引们
        if ~(isempty(indices1) | isempty(indices2))  %如果<=分裂值 >分裂值的样本数目都不等于0  
            tree.child(1)   = make_tree(patterns(dims, indices1), targets(indices1), inc_node, discrete_dim(dims), maxNbin, base+1);%递归 
            %对<=分裂值的样本进行再分叉
            tree.child(2)   = make_tree(patterns(dims, indices2), targets(indices2), inc_node, discrete_dim(dims), maxNbin, base+1);%递归 
            %对>分裂值的样本进行再分叉
        else  
            H               = hist(targets, length(Uc));  %统计当前所有样本的标签，分别属于每个标签的数目    H(1×标签数目)
            [m, largest]    = max(H);   %得到包含样本数最多的那个标签的索引，记为largest 包含多少个样本，记为m
            tree.child      = Uc(largest);  %姑且将这个特征的标签就记为包含样本数最多的那个标签  
            tree.dim                = 0;  %树的分裂特征记为0
        end  
    end  
