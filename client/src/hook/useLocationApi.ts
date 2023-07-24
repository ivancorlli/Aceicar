import { SelectOptions } from "@/component/Input/SearchSelect"



async function getLocation(search: string): Promise<SelectOptions[]> {
    const res = await fetch(`https://api.teleport.org/api/cities/?search=${search}`)
    const data = await res.json()
    let results: SelectOptions[] = []
    if (data != null) {
        if (data.count > 0) {
            const cities = data._embedded["city:search-results"]
            cities.map((e: any) => {
                let option: SelectOptions = {
                    value: e._links["city:item"].href,
                    label: e["matching_full_name"]
                }
                results.push(option)
            })
        }
    }
    return results
}

export {
    getLocation
}