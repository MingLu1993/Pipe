%% FBG数据预处理：求FBG应变响应
function output=preprocess(input)
output=1000*detrend(input)/1.2;
end