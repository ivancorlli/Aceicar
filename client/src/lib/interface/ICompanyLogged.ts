export default interface ICompanyLogged {
    companyId: string,
    name: string,
    publsihed: string
    picture?: string,
    description?: string,
    contactData?: {
        email: string,
        country: string,
        number: string
    }
    location?: {
        country: string,
        city: string,
        state: string,
        postalCode: string,
        street: string,
        streetNumber: string,
        floor: string,
        apartment: string
    }
}