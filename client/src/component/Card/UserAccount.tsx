import { Avatar, Text, VStack } from '@chakra-ui/react'
import React  from 'react'

interface IintialForm {
    username:string,
    phoneNumber :string
}
const intialForm:IintialForm ={
    username:"Nombre de usuario",
    phoneNumber:"Numero de telefono"
}

const UserAccount = ({form = intialForm}:{form?:IintialForm}) => {

  return (
        <>
            <VStack>
                <Avatar name={form.username ?? "Nombre de Usuario"} bg={form.username ? "brand.100" : "gray"} color="white" size="2xl" />
                <VStack spacing={0}>
                    <Text fontSize="lg" fontWeight="bold" >
                        {form.username.toLocaleUpperCase()}
                    </Text>
                    <Text fontSize="sm" color="gray" >
                        {form.phoneNumber}
                    </Text>
                </VStack>
            </VStack>
        </>
    )
}

export default UserAccount