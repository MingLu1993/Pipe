%对FBG信号进行5层小波分解
%对逼近系数和细节系数进行绝对值求和
function [a1,a2,a3,a4,a5,d1,d2,d3,d4,d5,a6,a7,d6,d7]=wavelet_S(s_FBG,n)
% s_FBG为输入FBG信号
% cla 小波基
% Sum_A 返回5层逼近系数绝对值和
% Sum_D 返回5层逼近系数绝对值和
switch(n)
case 1  
    cla='db1';
case 2  
    cla='db2';
case 3  
    cla='db3';
case 4  
    cla='db4';
case 5  
    cla='db5';
case 6  
    cla='db6';
case 7  
    cla='db7';
case 8  
    cla='db8';    
case 9  
    cla='db9';
case 10  
    cla='db10';
case 11
    cla='sym2';
case 12
    cla='sym3';
case 13
    cla='sym4';
case 14
    cla='sym5'; 
case 15
    cla='sym6';
case 16
    cla='sym7';
case 17
    cla='sym8';
case 18
    cla='haar'; 
case 19
    cla='coif1';
case 20
     cla='coif2';
case 21
     cla='coif3';
case 22
     cla='coif4';
case 23
     cla='coif5'
end

[c,l]=wavedec(s_FBG,7,cla);%重构第1-7层逼近系数
a7=wrcoef('a',c,l,cla,7);
a6=wrcoef('a',c,l,cla,6);
a5=wrcoef('a',c,l,cla,5);
a4=wrcoef('a',c,l,cla,4);
a3=wrcoef('a',c,l,cla,3);
a2=wrcoef('a',c,l,cla,2);
a1=wrcoef('a',c,l,cla,1);

d7=wrcoef('d',c,l,cla,7);
d6=wrcoef('d',c,l,cla,6);
d5=wrcoef('d',c,l,cla,5);
d4=wrcoef('d',c,l,cla,4);
d3=wrcoef('d',c,l,cla,3);
d2=wrcoef('d',c,l,cla,2);
d1=wrcoef('d',c,l,cla,1);

end




