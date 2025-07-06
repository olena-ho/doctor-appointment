using DoctorAppointmentDemo.Data.Configuration;
using DoctorAppointmentDemo.Data.Interfaces;
using DoctorAppointmentDemo.Domain.Entities;
using Newtonsoft.Json;

namespace DoctorAppointmentDemo.Data.Repositories;

public abstract class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : Auditable
{
    public abstract string Path { get; set; }

    public abstract int LastId { get; set; }

    public TSource Create(TSource source)
    {
        source.Id = ++LastId;
        source.CreatedAt = DateTime.Now;

        File.WriteAllText(Path, JsonConvert.SerializeObject(GetAll().Append(source), Formatting.Indented));
        SaveLastId();

        return source;
    }

    public bool Delete(int id)
    {
        if (GetById(id) is null)
            return false;

        File.WriteAllText(Path, JsonConvert.SerializeObject(GetAll().Where(x => x.Id != id), Formatting.Indented));

        return true;
    }

    public IEnumerable<TSource> GetAll()
    {
        var directory = System.IO.Path.GetDirectoryName(Path);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory!);
        }

        if (!File.Exists(Path))
        {
            File.WriteAllText(Path, "[]");
        }

        var json = File.ReadAllText(Path);

        if (string.IsNullOrWhiteSpace(json))
        {
            json = "[]";
            File.WriteAllText(Path, json);
        }

        return JsonConvert.DeserializeObject<List<TSource>>(json)!;
    }


    public TSource? GetById(int id)
    {
        return GetAll().FirstOrDefault(x => x.Id == id);
    }

    public TSource Update(int id, TSource source)
    {
        source.UpdatedAt = DateTime.Now;
        source.Id = id;

        File.WriteAllText(Path, JsonConvert.SerializeObject(GetAll().Select(x => x.Id == id ? source : x), Formatting.Indented));

        return source;
    }

    public abstract void ShowInfo(TSource source);

    protected abstract void SaveLastId();

    protected dynamic ReadFromAppSettings()
    {
        var fullPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppContext.BaseDirectory, Constants.AppSettingsPath));
        var json = File.ReadAllText(fullPath);
        return JsonConvert.DeserializeObject<dynamic>(json)!;
    }
}
