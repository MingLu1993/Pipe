%% 求信号的频谱
function [f,fft_data] = plotfft( x , fsample)
% INPUT PARAMETERS---------------------------------------------------------
% x:            输入信号
% fsample:      采样率
% OUTPUT VARIABLES---------------------------------------------------------
% f:            频率值
% fft_data:     幅值 
Fs=fsample;
%y=x-mean(x);        %去直流分量
% y=detrend(x);
L=length(x);         % FFT length
NFFT = 2^nextpow2(L); % Next power of 2 from length of 很容易理解
f=Fs/2*linspace(0,1,NFFT/2+1); % Frequency Vector
sig_fft=fft(x,NFFT)/NFFT;
fft_data=2*abs(sig_fft(1:NFFT/2+1));%plot(f,fft_data);grid on;
end

