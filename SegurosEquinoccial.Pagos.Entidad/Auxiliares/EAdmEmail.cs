﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Entidad.Auxiliares
{
    [DataContract]
    [Serializable]
    public class EAdmEmail
    {
        [DataMember]
        public string Para { get; set; }

        [DataMember]
        public string Asunto { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

    }
}
