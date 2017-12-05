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