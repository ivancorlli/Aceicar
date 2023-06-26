import React from 'react'

const GetAge = (date:Date) => {
    let age: number = 0
    const actualDate: Date = new Date()
    const userBirth: Date = date
    age = actualDate.getFullYear() - userBirth.getFullYear()
    if (actualDate.getMonth() < userBirth.getMonth()) {
        age = age - 1
    }        
    if (actualDate.getMonth() === userBirth.getMonth()&& (actualDate.getDate() < userBirth.getDate()) ){
        age = age - 1
    }
    return Math.abs(age)
}

export default GetAge