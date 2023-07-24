
enum Types {
    Maintenance="Maintenance",
    AutoPart="Auto Parts"
}


export default function ConvertTypes(Name:string):string
{
    Name =Name.toLowerCase().trim()
    let response = ""
    switch (Name) {
        case Types.Maintenance.toLowerCase():
            response= "Mantenimiento"
            break;
        case Types.AutoPart.toLowerCase():
            response = "Autopartes"    
        break;
        default:
            response = ""
            break;
    }
    return response;
}

