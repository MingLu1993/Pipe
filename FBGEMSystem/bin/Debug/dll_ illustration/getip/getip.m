%% �������źŵ�˲ʱ��λ
%----input��data----ԭʼ�ź�
%           fs------�źŲ�����
%----output��t----ʱ��
%           ip------˲ʱ��λ
function [t,ip]=getip(data,fs)
N=length(data);
t=linspace(0,N/fs,N);
x=hilbert(data);
xr=real(x);
xi=imag(x);
ip=atan2(xi,xr);
end