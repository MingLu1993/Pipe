function sub_predictVote = SubVote_Macro( t,subtree , sample ,macro,discrete_dim, targetsNum)
%UNTITLED Summary of this function goes here
%   Detailed explanation goes here
%   Subtree         1:t 树
%   sample          m个样本
%   macro            1:t 宏平均
%   discrete_dim    用于记录每一个特征是否是离散特征，初始化都记为0，代表都是连续特征;离散值存储的是离散的个数
%   targetsNum      类别个数
%   返回各样本的预测
    AllNum = size(sample,2);
    predict = zeros(t,AllNum);
    sub_predictVote = zeros(1,AllNum);
    sub_predictVote_array=zeros(targetsNum,1);
    for s=1:t
        predict(s,:)=use_tree(sample, 1:size(sample,2),subtree{s}, discrete_dim, targetsNum); 
    end
    
    for j=1:AllNum
        sub_predictVote_array(:,:)=0;
        for i=1:t
            sub_predictVote_array(predict(i,j))=sub_predictVote_array(predict(i,j))+log(macro(i)/(1-macro(i)));
        end
        [~,sub_predictVote(j)]=max(sub_predictVote_array);
    end

 end

