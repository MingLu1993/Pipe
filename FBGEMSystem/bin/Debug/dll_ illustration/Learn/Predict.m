function [ PredictVote1 ] = Predict( k,T, FinalTree,test_patterns,FinalBeta,FinalEnsembleBeta,discrete_dim, TargetsNum)
%UNTITLED5 Summary of this function goes here
%   Detailed explanation goes here
    PredictVote1=FinalVote_new(k,T, FinalTree,test_patterns',FinalBeta(1:k) ,FinalEnsembleBeta(1:k),discrete_dim, TargetsNum);
    %count1=sum(test_targets'==PredictVote1);%�õ���ʵ��Ԥ����ͬ����Ŀ 
    %accuracy1=count1/size(test_targets',2);  %������ȷ��
end

