
export default function Capitalize(name: string): string {
    let result = name
    let initial = name.charAt(0).toUpperCase()
    let rest = name.substring(1).toLowerCase()
    result = initial + rest;
    return result
}