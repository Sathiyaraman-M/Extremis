﻿namespace Extremis.Contracts;

public interface IEntity
{

}

public interface IEntity<TId> : IEntity
{
    TId Id { get; set; }
}