using System;
using System.ServiceModel;

namespace BatchMasivoServisioMDM.Servicio
{
    public class WFCServicioMDM
    {
        public void Insertar(string sendpoint, insert_Input sPayload)
        {
            try
            {
                BasicHttpBinding myBinding = new BasicHttpBinding(BasicHttpSecurityMode.None);
                myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                EndpointAddress myEndpoint = new EndpointAddress(sendpoint);
                ChannelFactory<msArticulosMDMPortType> myChannelFactory = new ChannelFactory<msArticulosMDMPortType>(myBinding, myEndpoint);
                msArticulosMDMPortType wcfClient = myChannelFactory.CreateChannel();
                wcfClient.insert(sPayload);
                ((IClientChannel)wcfClient).Close();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public void Update(string sendpoint, update_Input sPayload)
        {
            try
            {
                BasicHttpBinding myBinding = new BasicHttpBinding(BasicHttpSecurityMode.None);
                myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                EndpointAddress myEndpoint = new EndpointAddress(sendpoint);
                ChannelFactory<msArticulosMDMPortType> myChannelFactory = new ChannelFactory<msArticulosMDMPortType>(myBinding, myEndpoint);
                msArticulosMDMPortType wcfClient = myChannelFactory.CreateChannel();
                wcfClient.update(sPayload);
                ((IClientChannel)wcfClient).Close();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public void Delete(string sendpoint, delete_Input sPayload)
        {
            try
            {
                BasicHttpBinding myBinding = new BasicHttpBinding(BasicHttpSecurityMode.None);
                myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                EndpointAddress myEndpoint = new EndpointAddress(sendpoint);
                ChannelFactory<msArticulosMDMPortType> myChannelFactory = new ChannelFactory<msArticulosMDMPortType>(myBinding, myEndpoint);
                msArticulosMDMPortType wcfClient = myChannelFactory.CreateChannel();
                wcfClient.delete(sPayload);
                ((IClientChannel)wcfClient).Close();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}