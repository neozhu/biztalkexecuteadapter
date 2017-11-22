# BizTalk custom adapter

AssemblyExecuteAdapter

## 功能

更为方便的扩展BizTalk custom adapter 的交互方式，只需要实现IAssemblyExecute 接口就可以让BizTalk AssemblyExecuteAdapter 执行需要的业务逻辑。

## 代码

AssemblyExecuteAdapterTransmitterEndpoint.cs

通过配置需要加载的dll 文件来执行dll 内部处理逻辑
```javascript
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


        public class CSBSDK:IAssemblyExecute
    {
        public object GetInputParameter(System.Xml.XmlDocument xmldoc)
        {
            XDocument doc = XDocument.Parse(xmldoc.OuterXml);
            var   input = from el in doc.Descendants("InParameters")
                                      select new InputParameters
                                      {
                                          ak = el.Element("ak").Value,
                                          apiname = el.Element("apiname").Value,
                                          functionname = el.Element("functionname").Value,
                                          method = el.Element("method").Value,
                                          sk = el.Element("sk").Value,
                                           url = el.Element("url").Value,
                                          version = el.Element("version").Value,
                                          proxyurl = el.Element("proxyurl").Value
                                            }
                                           ;
            return input.First();
        }

        public System.IO.Stream ExecuteResponse(System.IO.Stream stream, object inputparameters)
        {
            var para = (InputParameters)inputparameters;
            csb.JAxis2Tunnel client = new csb.JAxis2Tunnel(para.proxyurl);
            csb.RequestEntity request = new csb.RequestEntity();
            request.ak = para.ak;
            request.apiname = para.apiname;
            request.functionname = para.functionname;
            request.method = para.method;
            request.sk = para.sk;
            request.url = para.url;
            request.version = para.version;
            stream.Seek(0, SeekOrigin.Begin);
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);
            request.jsonstring = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
            string response = client.getResult(request);
            var res = JsonConvert.DeserializeXmlNode(response, para.functionname + "_RES");
            Stream docstream=new MemoryStream();
            res.Save(docstream);
            Console.Write(res.OuterXml);
            docstream.Seek(0, SeekOrigin.Begin);
            return docstream;
        }
    }

    public class InputParameters { 
        public string ak{get;set;}
        public string apiname{get;set;}
        public string functionname{get;set;}
        public string method{get;set;}
        public string sk{get;set;}
        public string url{get;set;}
        public string version{get;set;}
        public string jsonstring{get;set;}
        public string proxyurl { get; set; }
          
    }
```

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
