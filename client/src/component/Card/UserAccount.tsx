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
                <Avatar name={form.username ?? "Nombre de Usuario"} bg="black" color="white" size="2xl" />
                <VStack>
                    <Text>
                        {form.username}
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