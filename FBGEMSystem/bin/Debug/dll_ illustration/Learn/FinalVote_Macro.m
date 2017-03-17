function PredictVote = FinalVote_Macro(K,T, FinalTree,patterns,Finalmacro ,FinalMACRO , discrete_dim, targetsNum)
    AllNum = size(patterns,2);
    PredictVote = zeros(1,AllNum);
    PredictVote_tmp = zeros(K,T,AllNum);
    predictVote_array=zeros(targetsNum,1);
    for k=1:K
        for t=1:T
            PredictVote_tmp(k,t,:) =SubVote_new(t,FinalTree{k},patterns ,Finalmacro{k},discrete_dim, targetsNum);
        end
    end
    
    for i=1:AllNum
        predictVote_array(:,:)=0;
        for kk=1:K
            for tt=1:T
                predictVote_array(PredictVote_tmp(kk,tt,i))=predictVote_array(PredictVote_tmp(kk,tt,i))...
                                                            +log(FinalMACRO{kk}(tt)/(1-FinalMACRO{kk}(tt)));
            end
        end
         [~,PredictVote(i)]=max(predictVote_array);
    end
%UNTITLED2 Summary of this function goes here
%   Detailed explanation goes here
%     AllNum = size(patterns,2);
%     PredictVote = zeros(1,AllNum);
%     predict = zeros(K,T,AllNum);
%     for k=1:K
%         Tree_k = FinalTree{k};
%         for t=1:T
%             predict(k,t,:)=use_tree(patterns, 1:size(patterns,2),Tree_k{t}, discrete_dim, targetsNum);
%         end
%     end
%     
%     predictVote_array=zeros(targetsNum,1);
%     for i=1:AllNum
%         predictVote_array(:,:)=0;
%         for kk=1:K
%             for tt=1:T
%                 predictVote_array(predict(kk,tt,i))=predictVote_array(predict(kk,tt,i))+FinalBeta{kk}(tt);
%             end
%         end
%         [~,PredictVote(i)]=max(predictVote_array);
%     end


end

