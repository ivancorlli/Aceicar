import { HStack, PinInput, PinInputField } from '@chakra-ui/react'
import React from 'react'

const EnterPin = () => {
    return (
        <HStack>
            <PinInput otp size="lg">
                <PinInputField />
                <PinInputField />
                <PinInputField />
                <PinInputField />
            </PinInput>
        </HStack>
    )
}

export default EnterPin