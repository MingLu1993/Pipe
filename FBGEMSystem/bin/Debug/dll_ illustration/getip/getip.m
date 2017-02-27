%% 求输入信号的瞬时相位
%----input：data----原始信号
%           fs------信号采样率
%----output：t----时间
%           ip------瞬时相位
function [t,ip]=getip(data,fs)
N=length(data);
t=linspace(0,N/fs,N);
x=hilbert(data);
xr=real(x);
xi=imag(x);
ip=atan2(xi,xr);
end