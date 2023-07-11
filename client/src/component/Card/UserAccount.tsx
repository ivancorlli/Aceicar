import { Avatar, Text, VStack } from '@chakra-ui/react'
import React  from 'react'

const UserAccount = ({form,image}:{form:{username:string,phoneNumber:string},image?:string}) => {

  return (
        <>
            <VStack>
                <Avatar src={image} name={image ? "" : form.username} color="white" size="xl" />
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