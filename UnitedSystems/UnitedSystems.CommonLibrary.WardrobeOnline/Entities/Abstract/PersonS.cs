﻿using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Abstract
{
    /// <summary>
    /// Класс-интерфейс маркер
    /// </summary>
    public abstract class PersonS : IEntityS
    {
        public abstract int ID { get; set; }
    }
}
