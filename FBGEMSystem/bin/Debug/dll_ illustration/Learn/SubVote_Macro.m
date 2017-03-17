function sub_predictVote = SubVote_Macro( t,subtree , sample ,macro,discrete_dim, targetsNum)
%UNTITLED Summary of this function goes here
%   Detailed explanation goes here
%   Subtree         1:t ��
%   sample          m������
%   macro            1:t ��ƽ��
%   discrete_dim    ���ڼ�¼ÿһ�������Ƿ�����ɢ��������ʼ������Ϊ0����������������;��ɢֵ�洢������ɢ�ĸ���
%   targetsNum      ������
%   ���ظ�������Ԥ��
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

