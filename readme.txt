液压管路LM
20170103 加入Message_Electric结构体，接收电类数据并显示；
20170104 test；
20170104 Test；
20170104 1556 pull test
20170105 数据实时显示修改：消除数据抖动；
20170105 修正实时显示波形界面，根据选择的通道设置通道下拉菜单，未设置通道提示先设置并弹出设置界面；
20170105 多人测试LM
20170105 在Receiver.cs文件中，加入解包线程，从解包缓存sharedDecodeEle中读取Message_Electric,解包成Message,放入绘图缓存sharedLocation1中
20170120 添加GlobalMembersFBG类，为FBG接收解析类（如何取出解析之后的数据还需设计）
	 在Receiver.cs文件中，去掉解包线程，删除解包缓存sharedDecodeEle，改为接一包解一包到绘图缓存sharedLocation1中；
	 设置界面改为tabControl控件分页，加入通信设置页面；主界面加入连接、开始、停止按钮；
	 连接按钮完成TCP连接至解析配置数据过程；开始按钮tcp发送“Z\n”并开启TCP、UDP接收线程；停止按钮还未实现；
	 Receiver类中加入Recv_FBG()函数，用于TCP接收线程。
20170120 在Message.cs中添加Message_FBG结构体，与原有系统中Message相同；
	 重命名与电类相关的Message结构体名字为Message_Electric（用于接收，一个数组），Message_EleDecoded（解包后保存，三个数组）
20170121 线程均改为IsBackground = true;FBG和电类的数据显示界面中修改，防止重复开线程。
20170121 17:28 电类波形显示、FBG和电类的数据显示界面中修改线程关闭方式，使用设置标志位的方式使线程函数跳出while执行完毕
	      （不使用Abort函数（通过引发线程异常来终止线程））；
	       未开启接收线程并接收到数据的时候，点开电类的数据显示界面，Receiver.msgDatashow.CH1为空引发异常，修改加入判断是否为空再赋值。