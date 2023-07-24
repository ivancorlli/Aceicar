export default interface ICompanyAccess {
    accessId: string,
    companyId: string,
    role: {
        roleId: string,
        name: string
    }
}