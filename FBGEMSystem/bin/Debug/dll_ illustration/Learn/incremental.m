function [Subtree,Beta,SubBt] = incremental( k,Subsample,T,rata,inc_node_begain,discrete_dim )
%Subsample  �Ӽ�
%T          ÿ���Ӽ���������
%rata       ÿ�ε���ѵ����TRռSubsample�ı���
%inc_node_begain            - Percentage of incorrectly assigned samples at a node  һ���ڵ���δ��ȷ����������İٷֱ�
%inc_nodeΪ��ֹ����ϣ���ʾ������С��һ����ֵ�����ݹ飬������Ϊ5-10
%ע��inc_node����̫��Ļ��ᵼ�·���׼ȷ���½���̫С�Ļ����ܻᵼ�¹����

%INCRETAL Summary of this function goes here
%   Detailed explanation goes here
    [m,Ni]= size(Subsample);  %m�Ǹ��Ӽ�������
    D=zeros(1,m);
    for i=1:m
        D(i)=1/m;
    end
    W=D;             %W(i)��ʾ����i��Ȩ��
    All_patterns=Subsample(:,1:(size(Subsample,2)-1))'; %���ڼ��������
    All_targets=Subsample(:,size(Subsample,2))';        %���ڼ��������
    
    Subtree=cell(1,T);
    Beta=zeros(1,T);      %ÿ��ѭ�������������滯������
    SubBt=zeros(1,T);     %ÿ��ѭ���ļ���Ht�����滯������
    
    %for t=1:T           %T�ε���
    t=1;
    while(t<=T)
        TTT=sum(W);
        D=W/TTT;      %���������ֲ�D
        
        while(true)
            %���ѡ��ѵ����TR�Ͳ��Լ�TE
            index=randperm(m);
            num=floor(m*rata);    %floor ����ȡ��
            %TR=zeros(num,Ni);
            %TE=zeros(m-num,Ni);
            indexTR=zeros(1,num);
            indexTE=zeros(1,m-num);
            for i=1:num              %numΪѵ��������
                indexTR(i)=index(i);    %ѵ��������
            end
            for i=num+1:m
                indexTE(i-num)=index(i);     %���Լ�����
            end
            TR=Subsample(indexTR,:);
            TE=Subsample(indexTE,:);

            train_patterns=TR(:,1:(size(TR,2)-1))';  %�õ�ѵ������
            train_targets=TR(:,size(TR,2))';   %�õ�ѵ�������ı�ǩ   1��ѵ��������Ŀ
            test_patterns=TE(:,1:(size(TE,2)-1))';  %�õ���������
            test_targets=TE(:,size(TE,2))';   %�õ�������������ʵ��ǩ������һ�����Ԥ���ǩ�Ա�
            x1=size(unique(train_targets),2);
            x2=size(unique(All_targets),2);
            if  size(unique(train_targets),2)==size(unique(All_targets),2)  %��֤ѵ�����������е����
                break
            end
        
        end
    
        [Ni, M]     = size(train_patterns); %M��ѵ����������Ni��ѵ������ά��������������Ŀ
        inc_node    = inc_node_begain*M/100;  % 5*ѵ��������Ŀ/100   !!!!
    
%         %������Щ����ģʽ(����)����ɢ�ģ�����ɢ����ģʽ�ϵ���Ӧά
%         discrete_dim = zeros(1,Ni); %���ڼ�¼ÿһ�������Ƿ�����ɢ��������ʼ������Ϊ0������������������
%         Nu=10;
%         %���������ģ�����ζ������ɢ���������ֵ�����Ϊ�����ɢ���������ظ�����ֵ����Ŀ 
%         for i = 1:Ni  %����ÿ������
%             Ub = unique(train_patterns(i,:));  %ȡÿ�������Ĳ��ظ�������ֵ���ɵ����� 
%             Nb = length(Ub);    %�õ����ظ�������ֵ����Ŀ
%             if (Nb <= Nu)  %���������������ظ�������ֵ����Ŀ�������ֵ��С������Ϊ�����������ɢ��  
%                 %This is a discrete pattern  
%                 discrete_dim(i) = Nb; %�õ�ѵ�������У�������������ظ�������ֵ����Ŀ �����discrete_dim(i)�У�i��ʾ��i������
%             end  
%         end 
        
        %fprintf('Building tree: subsample is %d;t is %d \n',k,t);  
        Subtree{t}   = make_tree(train_patterns, train_targets, inc_node, discrete_dim, max(discrete_dim), 0);
        

        Targets=unique(train_targets);
        TargetsNum=size(Targets,2);
        AllNum=size(All_patterns,2);
        %All_targets_predict_vote=zeros(1,AllNum);
        
        
        All_targets_predict_t   = use_tree(All_patterns, 1:size(All_patterns,2),Subtree{t}, discrete_dim,TargetsNum );
        
%         Epsilon=0;      %�˼���Ĵ�����
%         for i=1:AllNum
%             if All_targets_predict_t(i) ~=All_targets(i)
%                 Epsilon = Epsilon + D(i);
%             end
%         end

        Epsilon_index=find((All_targets_predict_t~=All_targets)==1);  %����Ԥ����������
        
        D_tmp_t=D(Epsilon_index);                                 %�����������D
        Epsilon=sum(D_tmp_t);
        
%         fprintf('subsample is %d;t is %d: Epsilon is %f\n',k,t,Epsilon);
        if Epsilon > 1/2
            fprintf('Building tree: subsample is %d;t is %d as Single again Epsilon is %f\n',k,t,Epsilon);
            t=t-1;
        else
            Beta(t)=Epsilon/(1-Epsilon);   %���滯������
            
            %ͨ��Ȩ��ͶƱ��ʽ�������ɼ���Ht
%             Epsilon_jicheng=0;          %���ɼ���Ĵ�����
%             error=0;
%             for i=1:AllNum
%                 %!!!!!!!!!!!!!!!!!????????????cell SubtreeΪʲô��()������{}
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
            Epsilon_jicheng_index=find((All_targets_predict_vote~=All_targets)==1);  %����Ԥ����������
            
            D_tmp=D(Epsilon_jicheng_index);                                 %�����������D
            Epsilon_jicheng=sum(D_tmp);
            [~,error]=size(Epsilon_jicheng_index);
            
            accuracy=1 - error/size(All_targets,2);  %������ȷ��
%             fprintf('����subsample is %d;t is %d��Դ˲��������ı�ǩԤ����ȷ��Ϊ%f\n',k,t,accuracy);
            
%             fprintf('subsample is %d;t is %d: Epsilon_jicheng is %f\n',k,t,Epsilon_jicheng);
            if Epsilon_jicheng >0.5
                fprintf('Building tree: subsample is %d;t is %d again as jicheng Epsilon_jicheng is %f\n',k,t,Epsilon_jicheng);
                t=t-1;
            else
                Bt=Epsilon_jicheng/(1-Epsilon_jicheng);
                SubBt(t)=Bt;
                Epsilon_jicheng_true_index=find((All_targets_predict_vote~=All_targets)==1);  %����Ԥ����ȷ������
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

       
        
%         c_count=sum(test_targets==test_targets_predict);%�õ���ʵ��Ԥ����ͬ����Ŀ 
%         accuracy=c_count/size(test_targets,2);  %������ȷ��
%         fprintf('subsample is %d;t is %d �ı�ǩԤ����ȷ��Ϊ%f\n',k,t,accuracy);
end


