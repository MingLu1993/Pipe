function tree = make_tree(patterns, targets, inc_node, discrete_dim ,maxNbin, base)  %
%         make_tree(train_patterns, train_targets, inc_node, discrete_dim, max(discrete_dim), 0);  
    %Build a tree recursively   �ݹ�ع�����
      
    [Ni, L]                     = size(patterns);  %%LΪ��ǰ������������NiΪ������Ŀ
    Uc                          = unique(targets);  %ѵ��������Ӧ���б��ǩ   ���ظ���ȡ����Щ��ǩ  Ҳ���ǵõ��б��ǩ�ĸ���
    tree.dim                    = 0;  %��ʼ�����ķ�������Ϊ��0��
    %tree.child(1:maxNbin)  = zeros(1,maxNbin);  
    tree.split_loc              = inf;  %��ʼ������λ����inf
      
    if isempty(patterns) 
        return  
    end  
      
    %When to stop: If the dimension is one or the number of examples is small 
    % inc_nodeΪ��ֹ����ϣ���ʾ������С��һ����ֵ�����ݹ飬������Ϊ5-10
    if ((inc_node > L) || (L == 1) || (length(Uc) == 1)) %���ʣ��ѵ������̫С(С��inc_node)����ֻʣһ������ֻʣһ���ǩ���˳�  
        H                   = hist(targets, length(Uc));  %ͳ�������ı�ǩ���ֱ�����ÿ����ǩ����Ŀ    H(1����ǩ��Ŀ)
        [m, largest]    = max(H); %�õ����������������Ǹ���ǩ����������Ϊlargest �������ٸ���������Ϊm
        tree.Nf         = [];  
        tree.split_loc  = [];  
        tree.child      = Uc(largest);%����ֱ�ӷ������а������������һ����Ϊ���ǩ 
        return  
    end  
      
    %Compute the node's I  
    for i = 1:length(Uc) %�����б��ǩ����Ŀ 
        Pnode(i) = length(find(targets == Uc(i))) / L;  %�õ���ǰ���������� ��ǩ=��i����ǩ ����������Ŀ  ռ���������ı���  �����Pnode(i)��
    end  
%   ���㵱ǰ����Ϣ�أ�����������Ϣ��
% 	���磬���ݼ�D����14��ѵ��������9���������Yes����5���������No����Inode = -9/14 * log2(9/14) - 5/14 * log2(5/14) = 0.940
    Inode = -sum(Pnode.*log(Pnode)/log(2));  
      
    %For each dimension, compute the gain ratio impurity ������ÿά���������ʵ������  ����������ÿ�������ֱ������Ϣ��
    %This is done separately for discrete and continuous patterns     ��������ɢ�������������ֿ�����
    delta_Ib    = zeros(1, Ni);  %Ni������ά��  ���ڼ�¼ÿ����������Ϣ������
    split_loc   = ones(1, Ni)*inf;  %��ʼ��ÿ�������ķ���ֵ��inf
      
    for i = 1:Ni %����ÿ������  
        data    = patterns(i,:);  %�õ���ǰ�����������������������ֵ
        Ud      = unique(data);   %�õ����ظ�������ֵ��������Ud
        Nbins   = length(Ud);     %�õ����ظ�������ֵ����Ŀ
        if (discrete_dim(i)) %��������������ɢ���� 
            %This is a discrete pattern  
            P   = zeros(length(Uc), Nbins);  %Uc���б��ǩ�ĸ���   �б��ǩ���������ظ�������ֵ����Ŀ
            for j = 1:length(Uc) %����ÿ����ǩ  
                for k = 1:Nbins %����ÿ������ֵ  
                    indices     = find((targets == Uc(j)) & (patterns(i,:) == Ud(k)));  
                    % &�����ھ������߼����� &&�����ã�ֻ�����ڵ���Ԫ�� &����˼Ҳ����
                    %�ҵ� ��������ǩ==��j����ǩ  ���� ��ǰ�����������������==��k������ֵ�� ����������
                    P(j,k)  = length(indices);  %��ΪP(j,k)
                end  
            end  
            Pk          = sum(P);  %ȡP��ÿһ�еĺͣ�Ҳ���ǵõ���ǰ���������У��������������ֵ==�������ֵ��������Ŀ   Pk(1������ֵ��Ŀ)��ʾ�������������ֵ����ÿ������ֵ��������Ŀ
            P1          = repmat(Pk, length(Uc), 1);  %��Pk���Ƴ� �б��ǩ���� ��
            P1          = P1 + eps*(P1==0);  %����Ҫ�ڱ�֤P1��������ʱ������0
            P           = P./P1;  %�õ���ǰ���������У����������ֵ����ÿ������ֵ�ұ�ǩ����ÿ����ǩ��������ռ��ǰ�������ֵ�е������ı���
            Pk          = Pk/sum(Pk);  %�õ���ǰ���������У����������ֵ����ÿ������ֵ��������ռ��ǰ���������ı���
            info        = sum(-P.*log(eps+P)/log(2));  %����������ÿ�������ֱ������Ϣ��  info(1������ֵ��Ŀ)
            delta_Ib(i) = (Inode-sum(Pk.*info))/(-sum(Pk.*log(eps+Pk)/log(2))); %����õ���ǰ��������Ϣ������
            %������Ϣ������(GainRatio)����ʽΪGain(A)/I(A),
            %����Gain(A)=Inode-sum(Pk.*info)��������A����Ϣ����
            %����I(A)=-sum(Pk.*log(eps+Pk)/log(2))Ϊ����A������������Ϣ
        else   %������������(��ҪҪ�ҵ����ʵķ���ֵ��ʹ������ɢ��)
            %This is a continuous pattern  
            P   = zeros(length(Uc), 2);  %   P(�б��ǩ��Ŀ��2)  ��1����ǰ..�������еı�ǩ�ֲ����   ��2�����ǰ..������֮��ı�ǩ�ֲ����  ���..���Ǹ�������λ��
      
            %Sort the patterns  
            [sorted_data, indices] = sort(data);  %data���ŵ��ǵ�ǰ����ѵ���������������������ֵ   ��С��������  sorted_data������õ����� indices������
            sorted_targets = targets(indices);  %��Ȼ���б��ǩҲҪ��������˳�����������
      
            %Calculate the information for each possible split  ���������Ϣ����
              I = zeros(1,Nbins);
              delta_Ib_inter    = zeros(1, Nbins);
              for j = 1:Nbins-1
                  P(:, 1) = hist(sorted_targets(find(sorted_data <= Ud(j))) , Uc);  %��¼<=��ǰ����ֵ�������ı�ǩ�ķֲ����
                  P(:, 2) = hist(sorted_targets(find(sorted_data > Ud(j))) , Uc);  %��¼>��ǰ����ֵ�������ı�ǩ�ķֲ����
                  Ps      = sum(P)/L;  %sum(P)�ǵõ�����λ��ǰ��ͺ������������ռ��ǰ���������ı���
                  P       = P/L;  %ռ���������ı���
                  Pk      = sum(P);   %%sum(P)�ǵõ�����λ��ǰ��ͺ�����ж��ٸ����� ���� 
                  P1      = repmat(Pk, length(Uc), 1);  %��Pk���Ƴ� �б��ǩ���� ��
                  P1      = P1 + eps*(P1==0);  
                  info    = sum(-P./P1.*log(eps+P./P1)/log(2));  %������Ϣ�أ�����������Ϣ��
                  I(j)    = Inode - sum(Ps.*info);  %Inode-sum(info.*Ps)�����Ե�j���������ѵĵ���Ϣ����   
                  delta_Ib_inter(j) =  I(j)/(-sum(Ps.*log(eps+Ps)/log(2))); %����õ���ǰ����ֵ����Ϣ������
              end

            [~, s] = max(I);  %�ҵ���Ϣ�������Ļ��ַ���  delta_Ib(i)�д�ŵ��Ƕ��ڵ�ǰ��i���������ԣ�������Ϣ������Ϊ�����������Ϣ����  s���������ַ���
            delta_Ib(i) = delta_Ib_inter(s);  %�õ������������ֵ��Ӧ����Ϣ������
            split_loc(i) = Ud(s);  %��Ӧ����i�Ļ���λ�þ�����ʹ��Ϣ�������Ļ���ֵ
        end  
    end  
      
    %Find the dimension minimizing delta_Ib    %�ҵ���ǰҪ��Ϊ��������������
    [m, dim]    = max(delta_Ib);  %�ҵ�����������������Ϣ�����Ӧ����������Ϊdim
    dims        = 1:Ni;  %Ni������Ŀ
    tree.dim    = dim;  %��Ϊ���ķ�������
      
    %Split along the 'dim' dimension  
    Nf      = unique(patterns(dim,:));  %�õ�ѡ��������Ϊ������������������һ��  Ҳ���ǵõ���ǰ�����������������������ֵ
    Nbins   = length(Nf);  %�õ�������������ظ�������ֵ����Ŀ
    tree.Nf = Nf;  %��Ϊ���ķ����������� ��ǰ�����������������������ֵ
    tree.split_loc      = split_loc(dim);  %����������Ļ���λ�ü�Ϊ���ķ���λ��  �������ѡ�����һ����ɢ������split_loc(dim)�ǳ�ʼֵinf��������
      
    %If only one value remains for this pattern, one cannot split it.  
    if (Nbins == 1)  %���ظ�������ֵ����Ŀ==1�����������ֻ����һ������ֵ���Ͳ��ܽ��з���
        H               = hist(targets, length(Uc));  %ͳ�Ƶ�ǰ���������ı�ǩ���ֱ�����ÿ����ǩ����Ŀ    H(1����ǩ��Ŀ)
        [m, largest]    = max(H);  %�õ����������������Ǹ���ǩ����������Ϊlargest �������ٸ���������Ϊm
        tree.Nf         = [];  %��Ϊ��������������з��ѣ�����Nf��split_loc��Ϊ��
        tree.split_loc  = [];  
        tree.child      = Uc(largest);  %���ҽ���������ı�ǩ�ͼ�Ϊ���������������Ǹ���ǩ
        return  
    end  
      
    if (discrete_dim(dim))  %�����ǰѡ��������Ϊ���������������Ǹ���ɢ���� 
        %Discrete pattern  
        for i = 1:Nbins   %����������������ظ�������ֵ����Ŀ
            indices         = find(patterns(dim, :) == Nf(i));  %�ҵ���ǰ�����������������������ֵΪNf(i)��������
            tree.child(i)   = make_tree(patterns(dims, indices), targets(indices), inc_node, discrete_dim(dims), maxNbin, base);%�ݹ�
            %��Ϊ���Ǹ���ɢ���������Էֲ��Nbins�����ֱ����ÿ������ֵ��������������ٷֲ�
        end  
    else  
        %Continuous pattern  %�����ǰѡ��������Ϊ���������������Ǹ��������� 
        indices1            = find(patterns(dim,:) <= split_loc(dim));  %�ҵ�����ֵ<=����ֵ��������������
        indices2            = find(patterns(dim,:) > split_loc(dim));   %�ҵ�����ֵ>����ֵ��������������
        if ~(isempty(indices1) | isempty(indices2))  %���<=����ֵ >����ֵ��������Ŀ��������0  
            tree.child(1)   = make_tree(patterns(dims, indices1), targets(indices1), inc_node, discrete_dim(dims), maxNbin, base+1);%�ݹ� 
            %��<=����ֵ�����������ٷֲ�
            tree.child(2)   = make_tree(patterns(dims, indices2), targets(indices2), inc_node, discrete_dim(dims), maxNbin, base+1);%�ݹ� 
            %��>����ֵ�����������ٷֲ�
        else  
            H               = hist(targets, length(Uc));  %ͳ�Ƶ�ǰ���������ı�ǩ���ֱ�����ÿ����ǩ����Ŀ    H(1����ǩ��Ŀ)
            [m, largest]    = max(H);   %�õ����������������Ǹ���ǩ����������Ϊlargest �������ٸ���������Ϊm
            tree.child      = Uc(largest);  %���ҽ���������ı�ǩ�ͼ�Ϊ���������������Ǹ���ǩ  
            tree.dim                = 0;  %���ķ���������Ϊ0
        end  
    end  
