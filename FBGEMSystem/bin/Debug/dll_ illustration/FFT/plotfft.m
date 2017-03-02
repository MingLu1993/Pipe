%% ���źŵ�Ƶ��
function [f,fft_data] = plotfft( x , fsample)
% INPUT PARAMETERS---------------------------------------------------------
% x:            �����ź�
% fsample:      ������
% OUTPUT VARIABLES---------------------------------------------------------
% f:            Ƶ��ֵ
% fft_data:     ��ֵ 
Fs=fsample;
%y=x-mean(x);        %ȥֱ������
% y=detrend(x);
L=length(x);         % FFT length
NFFT = 2^nextpow2(L); % Next power of 2 from length of ���������
f=Fs/2*linspace(0,1,NFFT/2+1); % Frequency Vector
sig_fft=fft(x,NFFT)/NFFT;
fft_data=2*abs(sig_fft(1:NFFT/2+1));%plot(f,fft_data);grid on;
end

