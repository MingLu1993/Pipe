function [ PredictVote1 ] = PredictMacro( k,T, FinalTree,test_patterns,Finalmecro,FinalMECRO,discrete_dim, TargetsNum)
%UNTITLED5 Summary of this function goes here
%   Detailed explanation goes here
    PredictVote1=FinalVote_Macro(k,T, FinalTree,test_patterns',Finalmecro(1:k) ,FinalMECRO(1:k),discrete_dim, TargetsNum);
    %count1=sum(test_targets'==PredictVote1);%得到真实和预测相同的数目 
    %accuracy1=count1/size(test_targets',2);  %计算正确率
end