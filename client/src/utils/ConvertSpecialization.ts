enum Specializations {
    AutomotiveServiceCenter = "Automotive Service Center",
    CarWash = "Car Wash"
}



export default function ConvertSpecializations(Name: string): string {
    Name = Name.toLowerCase().trim()
    let response = ""
    switch (Name) {
        case Specializations.AutomotiveServiceCenter.toLowerCase():
            response = "Lubricentro"
            break;
        case Specializations.CarWash.toLowerCase():
            response = "Lavadero"
            break;
        default:
            response = ""
            break;
    }
    return response;
}