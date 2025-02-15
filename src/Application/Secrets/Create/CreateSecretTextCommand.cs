﻿using Application.Abstractions.Messaging;

namespace Application.Secrets.Create;

public sealed class CreateSecretTextCommand : ICommand<Guid>
{
    public string SecretString  { get; set; }
    
    public int AmountOfViews  { get; set; }
    
    public int AmountOfDays  { get; set; }
    
    public bool UnlimitedViews  { get; set; }
    
    public bool UnlimitedTime  { get; set; }
}
