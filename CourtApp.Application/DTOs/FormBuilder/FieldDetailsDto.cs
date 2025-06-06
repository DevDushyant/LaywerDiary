﻿using System;

namespace CourtApp.Application.DTOs.FormBuilder
{
    public class FieldDetailsDto
    {
        public Guid Key { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string DefaultVal { get; set; }
        public bool IsRequire { get; set; }
        public int DispOrder { get; set; }
        public string Placeholder { get; set; }
        public FieldSizeDto FieldSize { get; set; }
    }
}
