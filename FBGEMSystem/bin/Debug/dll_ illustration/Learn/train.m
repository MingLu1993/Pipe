function [ FinalTree,FinalBeta,FinalEnsembleBeta ] = train( FinalTree,FinalBeta,FinalEnsembleBeta,k,Trains,T,rata,inc_node,discrete_dim )
%UNTITLED4 Summary of this function goes here
%   Detailed explanation goes here
    [Subtree,Beta,EnsembleBeta] = incremental( k,Trains,T,rata,inc_node,discrete_dim );
    FinalTree{k}=Subtree;
    FinalBeta{k}=Beta;
    FinalEnsembleBeta{k}=EnsembleBeta;
end