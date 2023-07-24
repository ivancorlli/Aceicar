export interface IUserAccess {
    accessId:string,
    role:{
        roleId:string,
        name:string
    },
    company:{
        companyId:string,
        name:string,
        picture?:string
    }
}