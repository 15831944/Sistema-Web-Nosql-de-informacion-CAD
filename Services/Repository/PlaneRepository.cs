using System.Linq;
using Model;
using Services;
using MongoDB.Driver;
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
        foreach (var item in collection.Find(new BsonDocument()).ToList())
            myPlaneList.Add(Adapt(item, false));

        return myPlaneList;
    }

    public Plane GetById(string myName)
    {
        return Adapt(collection.Find(Builders<BsonDocument>.Filter.Eq("Name", myName)).First(), true);
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

    public void Delete(string myName)
    {
        var deleteFilter = Builders<BsonDocument>.Filter.Eq("Name", myName);
        collection.DeleteOne(deleteFilter);
    }

    public Plane Adapt(BsonDocument myBsonDocument, bool WithFileContent)
    {
        Plane myPlane = new Plane()
        {
            Name = (string)myBsonDocument["Name"],
            Description = (string)myBsonDocument["Description"],
        };

        if (WithFileContent)
        {
            myPlane.FileContent = (byte[])myBsonDocument["FileContent"];
        }

        return myPlane;
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