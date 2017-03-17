function [ FinalTree,Finalmecro,FinalMECRO ] = trainMacro( FinalTree,Finalmecro,FinalMECRO,k,Trains,T,rata,inc_node,discrete_dim )
%UNTITLED4 Summary of this function goes here
%   Detailed explanation goes here
    [Subtree,macro,MACRO] = incrementalMacro( k,Trains,T,rata,inc_node,discrete_dim );
    FinalTree{k}=Subtree;
    Finalmecro{k}=macro;
    FinalMECRO{k}=MACRO;
end