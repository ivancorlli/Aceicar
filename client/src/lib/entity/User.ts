import { UserGender } from "../enum/UserGender";
import IUser, { Location, Profile } from "../interface/IUser";
import { Phone } from "../type/Phone";

export default class User implements IUser 
{
    UserId: string;
    Email: string;
    Username?: string | undefined;
    Phone?: Phone | undefined;
    Picture?: string | undefined;
    Profile?: Profile | undefined;
    Location?: Location | undefined;
    
    public constructor(userId:string,email:string)
    {
        this.UserId = userId;
        this.Email = email;
    }

    ChangePhone(phoneCountry:string,phoneNumber:string)
    {
        this.Phone = {
            Country:phoneCountry.trim(),
            Number:phoneNumber.trim(),
            Verified:false
        }
    }


    ChangeProfile(
        name:string,
        surname:string,
        gender:UserGender,
        birth:string
    )
    {
        this.Profile = {
            Name:name,
            Surname:surname,
            Gender:gender,
            Birth:birth
        }
    }

}