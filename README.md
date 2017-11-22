# BizTalk custom adapter

AssemblyExecuteAdapter

## 功能

更为方便的扩展BizTalk custom adapter 的交互方式，只需要实现IAssemblyExecute 接口就可以让BizTalk AssemblyExecuteAdapter 执行需要的业务逻辑。

## 代码

AssemblyExecuteAdapterTransmitterEndpoint.cs

通过配置需要加载的dll 文件来执行dll 内部处理逻辑

privateStream SendAssemblyExecuteAdapterRequest(IBaseMessage msg, AssemblyExecuteAdapterTransmitProperties config)

        {

                        VirtualStream responseStream = null;

            string charset = string.Empty;

            IBaseMessagePart bodyPart = msg.BodyPart;

            Stream btsStream;

            string messageid = msg.MessageID.ToString(&quot;D&quot;);

            if (null != bodyPart &amp;&amp; (null != (btsStream = bodyPart.GetOriginalDataStream())))

                        {

                try

                {

                    Type assemblyExecuteType = Type.GetType(config.AssemblyName);

                    IAssemblyExecute assemblyexecute = (IAssemblyExecute)Activator.CreateInstance(assemblyExecuteType);

                    object inputparameters = null;

                    if (!string.IsNullOrEmpty(config.InputParameterXml))

                    {

                        XmlDocument inputXml = newXmlDocument();

                        inputXml.LoadXml(config.InputParameterXml);

                        inputparameters = assemblyexecute.GetInputParameter(inputXml);

                    }

                    Stream stream = assemblyexecute.ExecuteResponse(btsStream, inputparameters);

                    #region saveresponsemessage

                    string responsefilename = string.Empty;

                    if (config.SaveResponseMessagePath != string.Empty &amp;&amp; config.SaveResponseMessagePath != &quot;N&quot;)

                    {

                        if (!Directory.Exists(config.SaveResponseMessagePath))

                            Directory.CreateDirectory(config.SaveResponseMessagePath);

                        responsefilename = Path.Combine(config.SaveResponseMessagePath, &quot;res\_&quot; + messageid + &quot;.txt&quot;);

                        SaveFile(responsefilename, stream);

                        stream.Seek(0, SeekOrigin.Begin);

                    }

                    #endregion

                    if (config.IsTwoWay)

                    {

                        responseStream = newVirtualStream(stream);

                    }

                }

                catch(Exception e)

                {

                    #region saveerrormessage

                    string errorfilename = string.Empty;

                    if (config.SaveErrorMessagePath != string.Empty &amp;&amp; config.SaveErrorMessagePath != &quot;N&quot;) {

                        if (!Directory.Exists(config.SaveErrorMessagePath))

                            Directory.CreateDirectory(config.SaveErrorMessagePath);

                        errorfilename = Path.Combine(config.SaveErrorMessagePath ,messageid + &quot;.txt&quot;);

                        SaveFile(errorfilename, btsStream);

                    }



                    #endregion

                    string Source = &quot;AssemblyExecuteAdapter&quot;;

                    string Log = &quot;Application&quot;;

                    string Event = e.Message + &quot;\r\n request message saved :&quot; + errorfilename;

                    if (!EventLog.SourceExists(Source))

                        EventLog.CreateEventSource(Source, Log);

                    EventLog.WriteEntry(Source, Event, EventLogEntryType.Error);

                    throw;

                }

                        }

            return responseStream;

        }



## 配置

配置发送端口

 ![](http://images2017.cnblogs.com/blog/5997/201711/5997-20171122095726102-1774234.png)

### 配置参数

 ![](http://images2017.cnblogs.com/blog/5997/201711/5997-20171122100326446-272874803.png) 

Assembly qualified name：实现了IAssemblyExecute接口的dll文件

Function Name: 这个adapter的功能名称，确保唯一

Input Parameter Xml: 执行ExecuteResponse需要的参数以XML的形式提供

Save Error Message Path：保存错误报文的路径

Save Response Message Path：保存执行ExecuteResponse方法返回的结果

### 选择实现了IAssemblyExecute 接口的dll文件

 ![](http://images2017.cnblogs.com/blog/5997/201711/5997-20171122095730946-23250115.png)

### 编辑输入参数

 ![](http://images2017.cnblogs.com/blog/5997/201711/5997-20171122095737477-684066601.png)
