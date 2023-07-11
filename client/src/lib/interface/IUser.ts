import { type } from "os"
import { UserGender } from "../enum/UserGender"
import { Phone } from "../type/Phone"

export type Profile = {
    Name:string,
    Surname:string,
    Gender:UserGender,
    Birth:string
}

export type Location = {
    Country:string,
    City:string,
    State:string,
    Street:string,
    StreetNumber:number,
    PostalCode:string
}


export default interface IUser {
    UserId:string
    Email:string
    Username?:string
    Phone?:Phone
    Picture?:string
    Profile?:Profile
    Location?:Location
}