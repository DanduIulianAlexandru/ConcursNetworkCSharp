using System.Collections.Generic;

namespace ConcursPersistence.repository; 

using ConcursModel.domain;

public interface IRepository<TID, E> where E : Entity<TID> {
    E FindOne(TID id);
    IEnumerable<E> FindAll();
    E Save(E entity);
    E Delete(TID id);
    E Update(E entity);
}