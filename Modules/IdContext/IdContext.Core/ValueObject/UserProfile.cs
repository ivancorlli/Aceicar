namespace IdContext.Core.ValueObject;

public class UserProfile
{ 
    public string Name {get; private set;} 
    public string Surname {get;private set;}
    public  string Gender {get;private set;} 
    public DateTime Birth {get;private set;} 

    public UserProfile(
        string name,
        string surname,
        string gender,
        DateTime birth

    )
    {
        Name = name.Trim();
        Surname = surname.Trim();
        Gender = gender.Trim();
        Birth = birth;
    }

    public string CompleteName()
    {
        return $"{Name} {Surname}";
    }

    public void ChangeGender(string gender)
    {
        Gender = gender;
    }

    public int CurrrentAge()
    {
        int age = (DateTime.Today.Year - Birth.Year);
        return age;
    }

}