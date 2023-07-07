import { UserGender } from '@/lib/enum/UserGender'
import GetAge from '@/utils/GetAge'
import { Avatar, HStack, Text, VStack } from '@chakra-ui/react'
import React from 'react'

interface IintialForm {
    name: string,
    surname: string,
    birth?: string,
    gender: string
}

const UserProfileHorizontal = ({ form,src }: { form: IintialForm,src?:string}) => {


    return (
        <HStack alignItems="center" justifyContent="center" spacing={8}>
            <Avatar name={!src?`${form.name} ${form.surname}`:""} src={src} bg={form.name ? "brand.100" : "gray"} color="white" size="lg" />
            <VStack spacing={0} alignItems="start">
                <Text fontSize="lg" fontWeight="bold" >
                    {form.name} {form.surname}
                </Text>
                {
                    form.birth ?
                        <Text fontSize="sm" color="gray" >
                            {GetAge(new Date(form.birth))} Años
                        </Text>
                        : <></>
                }
                <Text fontSize="sm" color="gray" >
                    {form.gender == UserGender.Male ? "Masculino" : "Femenino"}
                </Text>
            </VStack>
        </HStack>
    )
}

export default UserProfileHorizontal