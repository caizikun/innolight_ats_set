16:14 2014/6/18
1. ����CopyTestPlan�Ĺ��� [��Ҫѡ��һ�����е�TestPlan]
   �����ɵ�TestPlan��Ҫ����Ƿ����и�����!
   �����ɵ�TestEquipment��[ItemName]����Ҫ��������
2. ���ݵ�����Excel��������,��ͷ�ļ��޸�Ϊ����!�Ҳ���ʾ ID,PID,MGropuID...
3. ���ݿ�database·����� Ϊ 10.160.20.106\ATS_Home
4. GC.Collect();   //140618_2 ֻ��PNInfo.form�Ĳ��ֽ����ڴ����.
15:38 2014/6/19
1. ȷ��Copy���Լƻ�����OK!
2. �������Լƻ���Execl ���ܶ���Ŀ�Ĳ�����Ҫ���� TBD
11:19 2014/6/20
1. ����"�������Լƻ�"�ı�ͷ���ù���,ȷ��OK.
2. "�������Լƻ�"�ı���ʽ��ȷ��!
16:59 2014/6/20
1. �������µ�Accdb�����Ƴ�
15:46 2014/6/24
1. ��һ���ձ�������¼ʱϵͳ��ȡ�������һ������ID���ֽ����޸�...
14:17 2014/6/25
1. �������� conn.Close(); ���ж���������

16:19 2014/7/1
1. MGroupID--> MCoefsID
2. �޸ı���Ϊ: GlobalManufactureCoefficientsGroup	(ԭΪ"GlobalManufactureMemoryGroupTable")
3. �޸ı���Ϊ: GlobalManufactureCoefficients	(ԭΪ"GlobalManufactureMemory")
4. �޸�PN�������Ϣ��ʾ"..." ��ʾPN�µ�4������Ϣ!
11:13 2014/7/3
1. �����Ƴ��豸�б�TestModel BUG
2. ����AccessDB���������й��� 
3. TestPlan form��ʾ���PNʱˢ�����ϲ���BUG & 
4. ��TestModel��Ϊ����������ʾParameter
14:23 2014/7/4.
1. ����XML�����ļ�
2. SQLManager ���캯������ dbName ����
13:09 2014/7/7
1. ��������Global�������豸���ֽ���ȷ������!  //140707_0
17:09 2014/7/9
1. TestCtrl��TestPlan �����޸�����(˫�����ƺ�������øù���),�������������ϵ������ظ����,���ظ����������,����ᵼ��ɾ��������!
2. TestCtrl�������б������󲿷�Ĭ��ֵ����(�Ե�ǰ���ݿ���ڵ�Ϊ�ο�)[���������ݲ�֧��]
3. �����޸����빦��
4. ����ҳ������ѡ�����ݺ���ʾ��ǰ������Ϣ(����ά��ʱ����)
5. ����datagridview����������ʾ����,Ĭ�������������ʾ����
16:14 2014/07/14
1. ����ҳ������ �ж�currlst.SelectedIndex != -1   //140714_0
2. �����޸ĳɹ��������������
3. dgv�����ж� if (dgvEquipPrmtr.CurrentRow != null && dgvEquipPrmtr.CurrentRow.Index != -1) //140714_0
4. �豸���� �����ȴ��� 15 ���Զ������и�
5. mySqlIO = new AccessManager(Login.AccessFilePath);  //140714_1 ���캯���޸�
6. ����+ģ��+������ʾ�Ĳ��ֶ�ˢ�½��д��� RefreshMyInfo(bool isFormLoad)

17:19 2014/07/16
1. ��Excel������Ϊÿ����һ��sheet!
2. Login form��timer���ڵ�������ʱ���״̬������
14:22 2014/07/22
1. �޸�DATAIO�Ĺ�������!
2. ��������//��ȫ�Ĺ������ֿ�ʼ!++++++++++++++++++++++++++++++++++++++
    public class ServerDatabaseIO:DataIO	//SQL���� 2�������ķ���!
	{}
    public class LocalDatabaseIO : DataIO //�������ݿ����� 2�������ķ���!
	{}
15:19 2014/07/25
DATAIO�������� GetCurrTime
�����Ϊ���÷�������������

17:28 2014/09/12	//140912_0 
1. �޸�Login Form��void getLoginStutes()   //140912_0 ����[��ȡ��ǰָ��database���û�����]
2. Login Form����������Դѡ��ť,��������Ȼ�ȡXML�����ļ�,Ȼ����ܽ��е���...
3. BuildXml() ����Ĭ�ϵ�XMLʱ�޸�DBName��������;������� :ATSDBName �� EDVTDBName
4. ��PNInfo �� Login Form��txt���� ��ʾ��ǰ������Դ (ATSHome|EDVTHome)

11:05 2014/09/18
��XML�ļ������û�������ļ��ܺͽ��ܲ���!��δ����XML�ļ�,��Ĭ�Ͻ����µ��ļ�(�Ѿ�д���û�������)