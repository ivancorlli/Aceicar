
export type Phone = {
    CountryPhone: string,
    NumberPhone: string,
}

export type TimeZone = {
    Country: string,

}

export enum UserStatus {
    Active = 0,
    Inactive = 1,
    Suspended = 2,
    Deleted = 3
}

export enum UserGender {
    Male = 0,
    Female = 1
}

export interface UserAccount {
    UserId: string,
    Email: string,
    Status: UserStatus
    Username?: string
    Phone?: Phone
    ProfileImage?: string
    CreatedAt: number
    UpdatedAt: number
}

export interface UserProfile {
    Name: string,
    Surname: string
    Gender: UserGender,
    Birth: Date
}

export enum LocationStatus {
    Active = 0,
    Inactive = 1
}

export interface UserLocation {
    Country: string,
    City: string,
    State: string,
    PostalCode: string,
    Status?: LocationStatus
}

export interface User extends UserAccount {
    Profile?: UserProfile
    Location?: UserLocation
}
