using System.Linq;
using Model;
using Services;
using MongoDB.Driver;
using System;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Web.Mvc;

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

    public List<Plane> GetAll()
    {
        List<Plane> myPlaneList = new List<Plane>();
        List<BsonDocument> myList = collection.Find(new BsonDocument()).ToList();
        foreach (var item in myList)
        {
            myPlaneList.Add(Adapt(item));
        }

        return myPlaneList;
    }

    public void Save(Plane myPlane)
    {
        var doc = new BsonDocument
            {
                {"Name", myPlane.Name},
                {"Description", myPlane.Description},
                {"FileContent", myPlane.FileContent}
            };
        collection.InsertOne(doc);
    }

    public Plane Adapt(BsonDocument myBsonDocument)
    {
        return new Plane()
        {
            Name = (string)myBsonDocument["Name"],
            Description = (string)myBsonDocument["Description"],
            FileContent = (byte[])myBsonDocument["FileContent"]
        };
    }

    public bool Validate(ref ModelStateDictionary state, PlaneViewModel myViewModel)
    {
        Plane myPlane = myViewModel.Current;

        if (string.IsNullOrEmpty(myPlane.Name))
            state.AddModelError("1", "Debe introducir un nombre");

        if (string.IsNullOrEmpty(myPlane.Description))
            state.AddModelError("2", "Debe introducir una descripcion");

        if (myViewModel.PostedFile == null || myViewModel.PostedFile.ContentLength == 0)
            state.AddModelError("3", "Debe introducir un fichero válido");

        return state.IsValid;
    }

    #endregion
}