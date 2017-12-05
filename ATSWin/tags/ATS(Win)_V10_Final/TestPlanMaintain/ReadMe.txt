16:14 2014/6/18
1. 增加CopyTestPlan的功能 [需要选择一个已有的TestPlan]
   新生成的TestPlan需要检查是否已有该名称!
   新生成的TestEquipment的[ItemName]项需要重新命名
2. 数据导出到Excel功能完善,将头文件修改为中文!且不显示 ID,PID,MGropuID...
3. 数据库database路径变更 为 10.160.20.106\ATS_Home
4. GC.Collect();   //140618_2 只在PNInfo.form的部分进行内存回收.
15:38 2014/6/19
1. 确认Copy测试计划功能OK!
2. 导出测试计划到Execl 功能对项目的部分需要处理 TBD
11:19 2014/6/20
1. 增加"导出测试计划"的表头设置功能,确认OK.
2. "导出测试计划"的表格格式待确定!
16:59 2014/6/20
1. 将生成新的Accdb功能移除
15:46 2014/6/24
1. 当一个空表新增记录时系统获取到的最后一个插入ID部分进行修改...
14:17 2014/6/25
1. 修正部分 conn.Close(); 的判断条件问题

16:19 2014/7/1
1. MGroupID--> MCoefsID
2. 修改表名为: GlobalManufactureCoefficientsGroup	(原为"GlobalManufactureMemoryGroupTable")
3. 修改表名为: GlobalManufactureCoefficients	(原为"GlobalManufactureMemory")
4. 修改PN后面的信息显示"..." 显示PN下的4个表信息!
11:13 2014/7/3
1. 修正移除设备列表TestModel BUG
2. 本机AccessDB将满足所有功能 
3. TestPlan form显示多个PN时刷新资料不对BUG & 
4. 若TestModel不为新增则不再显示Parameter
14:23 2014/7/4.
1. 导入XML配置文件
2. SQLManager 构造函数新增 dbName 参数
13:09 2014/7/7
1. 对于新增Global参数的设备部分进行确认修正!  //140707_0
17:09 2014/7/9
1. TestCtrl和TestPlan 可以修改名称(双击名称后可以启用该功能),但增加新增资料的名称重复检查,若重复则清空名称,否则会导致删数据问题!
2. TestCtrl的下拉列表新增大部分默认值载入(以当前数据库存在的为参考)[其他属性暂不支持]
3. 新增修改密码功能
4. 部分页面新增选择内容后显示当前内容信息(用与维护时复制)
5. 部分datagridview进行内容显示调整,默认填充满数据显示部分
16:14 2014/07/14
1. 部分页面增加 判定currlst.SelectedIndex != -1   //140714_0
2. 密码修改成功后隐藏新密码框
3. dgv增加判定 if (dgvEquipPrmtr.CurrentRow != null && dgvEquipPrmtr.CurrentRow.Index != -1) //140714_0
4. 设备参数 若长度大于 15 则自动调节行高
5. mySqlIO = new AccessManager(Login.AccessFilePath);  //140714_1 构造函数修改
6. 流程+模型+参数显示的部分对刷新进行处理 RefreshMyInfo(bool isFormLoad)

17:19 2014/07/16
1. 将Excel导出分为每个表一个sheet!
2. Login form的timer对于低于闲置时间对状态栏隐藏
14:22 2014/07/22
1. 修改DATAIO的公共部分!
2. 新增子类//完全的公共部分开始!++++++++++++++++++++++++++++++++++++++
    public class ServerDatabaseIO:DataIO	//SQL子类 2个公共的方法!
	{}
    public class LocalDatabaseIO : DataIO //本地数据库子类 2个公共的方法!
	{}
15:19 2014/07/25
DATAIO基类新增 GetCurrTime
代码改为引用服务器公共部分

17:28 2014/09/12	//140912_0 
1. 修改Login Form的void getLoginStutes()   //140912_0 方法[获取当前指定database的用户数量]
2. Login Form的新增数据源选择按钮,当点击后先获取XML配置文件,然后才能进行登入...
3. BuildXml() 建立默认的XML时修改DBName子项资料;变更属性 :ATSDBName 和 EDVTDBName
4. 在PNInfo 和 Login Form的txt属性 显示当前的数据源 (ATSHome|EDVTHome)

11:05 2014/09/18
对XML文件新增用户和密码的加密和解密部分!若未发现XML文件,在默认建立新的文件(已经写入用户和密码)
17:30 2014/10/21. 
增加保存登入和修改Log信息的部分[上传到Server的部分需要完善]
12:32 2014/10/22
1. MaintainPlan/CtrlInfo.cs : 增加对其他属性的长度判定若<=0 则新增
2. 新增UserLoginInfo 表存放登入和修改记录...
3. 增加部分防呆处理...[ModelInfo]
4. SQLDataIO.cs -->更新时重新计算当前的设备编号和设备名称
5. PNInfo-->  TopoToatlDSAcceptChanges()增加记录修改的内容

15:15 2014/10/28
1. 新增设备表的 设备初始化顺序[数据库目前默认是1,当开启程式进入后自动将SEQ改为1-N]
2. ctrl和Model表新增字段chkIgnoreFlag<属否忽略当前项目
3. 可以重置设备名称[但是在当前TestPlan唯一]
9:13 2014/10/29
1. 将TRX 改为NA...
2. 取消更新前强制检查设备名称与ID对应的部分
3.  取消登入界面选择数据库类型[SQL]的部分,直接从xml文件获取
10:20 2014/10/30
Equipment 新增字段"Role" 存放 0:NA,1:TX,2:RX
对应的查询条件变更为: ItemName+PID+SEQ,新增删除条件变更为查找 ID;
12:42 2014/10/30
改变调整设备顺序的方法对filterStr2的顺序变更部分
void ChangeSEQ(int direction, ListBox myList, DataTable dt, string filterStr1, string filterStr2)
10:43 2014/10/31
1. 取消PNInfo.cs 和Login.cs中不必要的注释部分
2. 修改主界面载入的显示方式:myPNInfo.ShowDialog();
3. 因为修改了设备名称:出现重复将误删,修改查询条件
4. 修正部分界面未新增完成删除资料的部分改为将新增标识改为false;
14:04 2014/10/31
修改前后的资料记录ID号
if (ss1.Length <= 0) ss1 += "修改前资料为:" + "ID=" + dataRow["ID"].ToString();
if (ss2.Length <= 0) ss2 += "修改前资料为:" + "ID=" + dataRow["ID"].ToString();

16:49 2014/10/31
1. 登入程序时不再检查登入用户数目:getLoginStutes()
2. 增加提交数据失败的ErrorLog记录
11:01 2014/11/04
DefaultLowLimit -->SpecMin
DefaultUpperLimit-->SpecMax

Equipment和TestParameter增加点击关闭时提示确认离开选择?
11:04 2014/11/06
1.转换为英文界面
2. testplan 改为不能删除!而是忽略

9:31 2014/11/12
1. 操作记录按testplan分别记录! 并存放于表:[OperationLogs]
15:23 2014/11/12
1. 修正部分英文化资料.
14:21 2014/11/19
1. 增加存操作日志的修改时间 且操作日志为空白时不再存档.
2. 更新时增加判断是否有资料改变!
3. 将formload部分内容变更.
16:14 2014/11/25
1. 登入界面增加回车自动登入,增加判定密码错误次数!
2. 增加判定文本长度...
3. 变更部分ComboBox为单独下拉
4. 增加TestModel的参数描述栏位
10:54 2014/11/28
1. 修正获取TestPlan PID和系数PID循环次数不够的问题!
2. 编辑重命名TestPlan 和FlowControl的判定方式变更!

13:16 2014/12/03
1. 将Login界面变为统一公共部分[需要同步修改Config.XML资料],命名空间统一改为 :Maintain
2. 调整部分变量的访问修饰符为 private,统一登入后显示的窗体为 :MainForm
14:12 2014/12/04
新增跨数据源复制TestPlan的功能![数据源支持Accdb+SQLServer]
9:12 2014/12/09
1. 修正部分遗漏的英文显示
2. TestModelParameter改为清除资料时载入预设的下拉清单[0,1]
10:24 2014/12/11
对于TestModelParameter SpecMax=32767或SpecMin=-32768 无需进行检查类型是否相符!
15:18 2014/12/18
1. Log记录的部分:单独修改TestModel或TestParameter时也记录当前的FlowCtrl名称<方便查看>
2. Equip 保存资料时衰减器确认输入的ATTSlot格式
3. 版本升级为1.2.0
14:05 2014/12/25
选择Type后点击"..."按钮增加显示Type的部分MSA信息!
选择PN后点击"..."按钮,增加显示PN信息!
9:50 2015/01/04
1.TopoTestPlan表新增字段 [IsChipInitialize],用于判定是否需要装置初始化;
  TopoTestPlan界面新增[IsChipInit](若当前表包含字段 [IsChipInitialize]则显示,否则不显示)
  ,且在保存资料时不再检查Aux未填写值.
16:08 2015/01/14
1. 优化在存储操作日志的部分查询条件
2. 被隐藏显示在主界面的TestPlan可以在TestPlan界面重新启用
3. TestPlan界面新增功能以维护是否需要备份系数资料.
15:10 2015/01/15
验证权限部分修改...
9:59 2015/01/19
同步修改:
1. 修改引用: 新增引用 Microsoft.Office.Interop.Access;
   撤销对ADOX的引用
9:24 2015/02/03
1. 升级版本为1.0.2.2
2. 对Config.XML的key资料进行处理,防止 为 null 的部分导致报错!
3. 将PN的"..."和Type的"..."增加设置排序按page,addr显示!不再允许用户重新排序!
16:20 2015/02/03
1. 新的ATSDebug数据库架构变更支持信息维护更新
 a. 表名GlobalMSAEEPROMInitialize为 TopoMSAEEPROMSet.
 b. TopoManufactureConfigInit.PID= TopoTestPlan.ID
 c. TopoTestPlan 新增字段IsEEPROMInitialize: 是否需要初始化ManufactureEEPROM资料
 d. 导出TestPlan资料到Excel更新.
 e. SQLDataIO.cs防止空表时获取自增量ID报错问题!
 f. 新增TestPlan时保存后用户需要自助选择进入 MConfigInit的维护 还是Equipment维护,不再自动跳转
12:31 2015/02/09
版本升级为 1.0.2.3
1. 增加对部分datagridview进行默认排序的部分和键盘光标的事件响应!
2. 新增MConfig参数资料好保存焦点会落在dgv对应的新资料.
16:54 2015/02/27
1. 修正在新增TestPlan时获取TestPlanID报错的问题!
2. 部分查询条件指定 ID= myLoginID
3. 更新资料时UpdateTable方法增加判定当前dt是否有资料被修改!
10:52 2015/03/05
升级版本为: 1.0.3.0
1. TestCtrlForm.cs变更刷新对应显示部分~
2. MainForm 鼠标移至sslRunMsg上方时显示当前的sslRunMsg信息
15:36 2015/03/25
升级版本为: 1.0.3.1
1. FLEXDCA对于O/E通道的限制更新为:"0,0,0,0"或"1A,1B,1C,1D"方式可以保存
16:16 2015/04/11
升级版本为: 1.0.3.2
1. 新增了复制指定FollowControl的功能!(必须是数据库已经存在的资料)
2. 新增功能可以覆盖Mdoel增加字段[FailBreak]和ModelParameter删除字段[FailBreak];
3. 查询Type/PN时增加条件判断是否存在字段[IgnoreFlag]
4. 新增设备时若未选择该设备的功能则预设为"NA";
5. TestPlan,TestCtrl,TestModel页面增加描述ItemDescription(TestPlan,TestCtrl:若数据库存在字段则显示)
10:40 2015/04/14
1. TopoTestControl 界面新增字段描述当前属于那个类别"FMT/LP/Both",数据库存储为"2,1,3";