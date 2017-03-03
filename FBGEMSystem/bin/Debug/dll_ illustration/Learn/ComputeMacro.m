function Macro = ComputeMacro( Ture_All_targets,Predict_All_targets )
%Ture_All_targets:样本真实分类
%Predict_All_targets:样本预测分类
C=size(unique(Ture_All_targets),2);
F1=zeros(1,C);

index = find((Ture_All_targets==Predict_All_targets)==1); %所有预测正确的样本的索引
for c=1:C
    TP = sum(Ture_All_targets(index)==c);
    Predict_c = sum(Predict_All_targets==c);
    True_c = sum(Ture_All_targets==c);
    if Predict_c~=0
        p=TP/Predict_c;
    else
        p=1;
    end
    
    if True_c~=0
        r=TP/True_c;
    else
        r=1;
    end
    if (p==0&&r==0)
        F1(c)=0;
    else
        F1(c)=2*p*r/(p+r);
    end
end
Macro=sum(F1)/C;

end