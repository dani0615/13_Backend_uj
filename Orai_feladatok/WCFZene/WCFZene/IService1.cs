using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCFZene.Models;

namespace WCFZene
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        //CRUD Operations for Eloado entity
        [OperationContract]
        //Create
        string InsertEloado(Eloado eloado);
        //read
        [OperationContract]
        List<Eloado> GetEloadok();

        //Update
        [OperationContract]
        string UpdateEloado(Eloado eloado);

        //Delete
        [OperationContract]
        string DeleteEloado(int id);

        string GetData(int value);


       

        // TODO: Add your service operations here
    }


    
    
}
