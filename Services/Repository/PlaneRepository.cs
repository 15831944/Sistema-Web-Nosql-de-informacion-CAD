using System.Linq;
using Model;
using Services;
using MongoDB.Driver;
using System;
using MongoDB.Bson;
using System.Collections.Generic;

public class PlaneRepository : BaseRepository
{
    #region Variables and Properties
    private IMongoCollection<BsonDocument> collection;
    #endregion

    #region Constructor

    public PlaneRepository(CadEntities myDbContext, LoggingService myLoggingService) : base(myDbContext, myLoggingService)
    {
        InitializeCollectionConnection();
    }

    #endregion

    #region Public Methods   

    private void InitializeCollectionConnection()
    {
        MongoClient dbClient = new MongoClient("mongodb://localhost:27017/");
        var database = dbClient.GetDatabase("CADPlane");
        collection = database.GetCollection<BsonDocument>("CADPlane");
    }

    public List<ActionWrapper> GetAll()
    {
        List<ActionWrapper> myPlaneList = new List<ActionWrapper>();
        List<BsonDocument> myList = collection.Find(new BsonDocument()).ToList();
        foreach (var item in myList)
        {
            myPlaneList.Add(Adapt(item));
        }

        return myPlaneList;
    }

    public ActionWrapper Adapt(BsonDocument myBsonDocument)
    {
        return new ActionWrapper();
    }

    #endregion
}