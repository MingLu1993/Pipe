%% 对输入信号进行多重分形去趋势（MFDFA）分析
function [Hq,tq,alpha,f,q] = mfdfa(signal)
%
% INPUT PARAMETERS---------------------------------------------------------
% 
% signal:       input signal
% m:            polynomial order for the detrending

% OUTPUT VARIABLES---------------------------------------------------------
% 
% Hq:           q-order Hurst exponent
% tq:           q-order mass exponent 
% alpha:        q-order singularity exponent
% f:            q-order dimension 
% q:            q-order that weights the local variations

scmin=16;
scmax=1024;
scres=19;
exponents=linspace(log2(scmin),log2(scmax),scres);
scale=round(2.^exponents);
q=linspace(-10,10,101);
m=1;
warning off
X=cumsum(signal-mean(signal));
if min(size(X))~=1||min(size(scale))~=1||min(size(q))~=1;
    error('Input arguments signal, scale and q must be a vector');
end
if size(X,2)==1;
   X=transpose(X);
end
if min(scale)<m+1
   error('The minimum scale must be larger than trend order m+1')
end
for ns=1:length(scale),
    segments(ns)=floor(length(X)/scale(ns));
    for v=1:segments(ns),
        Index=((((v-1)*scale(ns))+1):(v*scale(ns)));
        C=polyfit(Index,X(Index),m);
        fit=polyval(C,Index);
        RMS_scale{ns}(v)=sqrt(mean((X(Index)-fit).^2));
    end
    for nq=1:length(q),
        qRMS{nq,ns}=RMS_scale{ns}.^q(nq);
        Fq(nq,ns)=mean(qRMS{nq,ns}).^(1/q(nq));
    end
    Fq(q==0,ns)=exp(0.5*mean(log(RMS_scale{ns}.^2)));
end
% YMatrix1=log2(Fq);
for nq=1:length(q),
    C = polyfit(log2(scale),log2(Fq(nq,:)),1);
    Hq(nq) = C(1);
    qRegLine{nq} = polyval(C,log2(scale));
end
tq = Hq.*q-1;
alpha = diff(tq)./(q(2)-q(1));
f = (q(1:end-1).*alpha)-tq(1:end-1);
end

