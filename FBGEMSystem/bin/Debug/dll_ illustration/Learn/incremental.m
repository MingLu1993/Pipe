function [Subtree,Beta,SubBt] = incremental( k,Subsample,T,rata,inc_node_begain,discrete_dim )
%Subsample  子集
%T          每个子集迭代次数
%rata       每次迭代训练集TR占Subsample的比例
%inc_node_begain            - Percentage of incorrectly assigned samples at a node  一个节点上未正确分配的样本的百分比
%inc_node为防止过拟合，表示样本数小于一定阈值结束递归，可设置为5-10
%注意inc_node设置太大的话会导致分类准确率下降，太小的话可能会导致过拟合

%INCRETAL Summary of this function goes here
%   Detailed explanation goes here
    [m,Ni]= size(Subsample);  %m是该子集样本数
    D=zeros(1,m);
    for i=1:m
        D(i)=1/m;
    end
    W=D;             %W(i)表示样本i的权重
    All_patterns=Subsample(:,1:(size(Subsample,2)-1))'; %用于计算错误率
    All_targets=Subsample(:,size(Subsample,2))';        %用于计算错误率
    
    Subtree=cell(1,T);
    Beta=zeros(1,T);      %每次循环的子树的正规化错误率
    SubBt=zeros(1,T);     %每次循环的集成Ht的正规化错误率
    
    %for t=1:T           %T次迭代
    t=1;
    while(t<=T)
        TTT=sum(W);
        D=W/TTT;      %建立样本分布D
        
        while(true)
            %随机选择训练集TR和测试集TE
            index=randperm(m);
            num=floor(m*rata);    %floor 向下取整
            %TR=zeros(num,Ni);
            %TE=zeros(m-num,Ni);
            indexTR=zeros(1,num);
            indexTE=zeros(1,m-num);
            for i=1:num              %num为训练集个数
                indexTR(i)=index(i);    %训练集索引
            end
            for i=num+1:m
                indexTE(i-num)=index(i);     %测试集索引
            end
            TR=Subsample(indexTR,:);
            TE=Subsample(indexTE,:);

            train_patterns=TR(:,1:(size(TR,2)-1))';  %得到训练样本
            train_targets=TR(:,size(TR,2))';   %得到训练样本的标签   1×训练样本数目
            test_patterns=TE(:,1:(size(TE,2)-1))';  %得到测试样本
            test_targets=TE(:,size(TE,2))';   %得到测试样本的真实标签，用于一会儿和预测标签对比
            x1=size(unique(train_targets),2);
            x2=size(unique(All_targets),2);
            if  size(unique(train_targets),2)==size(unique(All_targets),2)  %保证训练集中有所有的类别
                break
            end
        
        end
    
        [Ni, M]     = size(train_patterns); %M是训练样本数，Ni是训练样本维数，即是特征数目
        inc_node    = inc_node_begain*M/100;  % 5*训练样本数目/100   !!!!
    
%         %查找哪些输入模式(特征)是离散的，并离散测试模式上的相应维
%         discrete_dim = zeros(1,Ni); %用于记录每一个特征是否是离散特征，初始化都记为0，代表都是连续特征，
%         Nu=10;
%         %如果后面更改，则意味着是离散特征，这个值会更改为这个离散特征的无重复特征值的数目 
%         for i = 1:Ni  %遍历每个特征
%             Ub = unique(train_patterns(i,:));  %取每个特征的不重复的特征值构成的向量 
%             Nb = length(Ub);    %得到无重复的特征值的数目
%             if (Nb <= Nu)  %如果这个特征的无重复的特征值的数目比这个阈值还小，就认为这个特征是离散的  
%                 %This is a discrete pattern  
%                 discrete_dim(i) = Nb; %得到训练样本中，这个特征的无重复的特征值的数目 存放在discrete_dim(i)中，i表示第i个特征
%             end  
%         end 
        
        %fprintf('Building tree: subsample is %d;t is %d \n',k,t);  
        Subtree{t}   = make_tree(train_patterns, train_targets, inc_node, discrete_dim, max(discrete_dim), 0);
        

        Targets=unique(train_targets);
        TargetsNum=size(Targets,2);
        AllNum=size(All_patterns,2);
        %All_targets_predict_vote=zeros(1,AllNum);
        
        
        All_targets_predict_t   = use_tree(All_patterns, 1:size(All_patterns,2),Subtree{t}, discrete_dim,TargetsNum );
        
%         Epsilon=0;      %此假设的错误率
%         for i=1:AllNum
%             if All_targets_predict_t(i) ~=All_targets(i)
%                 Epsilon = Epsilon + D(i);
%             end
%         end

        Epsilon_index=find((All_targets_predict_t~=All_targets)==1);  %查找预测错误的索引
        
        D_tmp_t=D(Epsilon_index);                                 %错误的样本的D
        Epsilon=sum(D_tmp_t);
        
%         fprintf('subsample is %d;t is %d: Epsilon is %f\n',k,t,Epsilon);
        if Epsilon > 1/2
            fprintf('Building tree: subsample is %d;t is %d as Single again Epsilon is %f\n',k,t,Epsilon);
            t=t-1;
        else
            Beta(t)=Epsilon/(1-Epsilon);   %正规化错误率
            
            %通过权重投票方式产生集成假设Ht
%             Epsilon_jicheng=0;          %集成假设的错误率
%             error=0;
%             for i=1:AllNum
%                 %!!!!!!!!!!!!!!!!!????????????cell Subtree为什么用()而不是{}
%                 sub_predictVote= SubVote( t,Subtree(1:t) , All_patterns(:,i) ,Beta(1:t),discrete_dim, TargetsNum);
%                 %[~, s] = max(sub_predictVote);
%                 All_targets_predict_vote(i)=sub_predictVote;
%                 
%                 if sub_predictVote ~=All_targets(i)
%                     Epsilon_jicheng = Epsilon_jicheng + D(i);
%                     error=error+1;
%                 end  
%             end

            All_targets_predict_vote= SubVote_new( t,Subtree(1:t) , All_patterns ,Beta(1:t),discrete_dim, TargetsNum);
            Epsilon_jicheng_index=find((All_targets_predict_vote~=All_targets)==1);  %查找预测错误的索引
            
            D_tmp=D(Epsilon_jicheng_index);                                 %错误的样本的D
            Epsilon_jicheng=sum(D_tmp);
            [~,error]=size(Epsilon_jicheng_index);
            
            accuracy=1 - error/size(All_targets,2);  %计算正确率
%             fprintf('集成subsample is %d;t is %d针对此测试样本的标签预测正确率为%f\n',k,t,accuracy);
            
%             fprintf('subsample is %d;t is %d: Epsilon_jicheng is %f\n',k,t,Epsilon_jicheng);
            if Epsilon_jicheng >0.5
                fprintf('Building tree: subsample is %d;t is %d again as jicheng Epsilon_jicheng is %f\n',k,t,Epsilon_jicheng);
                t=t-1;
            else
                Bt=Epsilon_jicheng/(1-Epsilon_jicheng);
                SubBt(t)=Bt;
                Epsilon_jicheng_true_index=find((All_targets_predict_vote~=All_targets)==1);  %查找预测正确的索引
                D(Epsilon_jicheng_true_index)=D(Epsilon_jicheng_true_index)*Bt;
                D(Epsilon_jicheng_index)= D(Epsilon_jicheng_index);
%                 for i=1:AllNum
%                     if All_targets_predict_vote(i) == All_targets(i)
%                         W(i)=W(i)*Bt;
%                     else
%                         W(i)=W(i);
%                     end
%                 end
            end
        end
        
        t=t+1;
    end

       
        
%         c_count=sum(test_targets==test_targets_predict);%得到真实和预测相同的数目 
%         accuracy=c_count/size(test_targets,2);  %计算正确率
%         fprintf('subsample is %d;t is %d 的标签预测正确率为%f\n',k,t,accuracy);
end


