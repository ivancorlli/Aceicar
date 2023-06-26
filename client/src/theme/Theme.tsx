import { extendTheme, type ThemeConfig } from "@chakra-ui/react";

const config: ThemeConfig = {
    initialColorMode: 'light',
    useSystemColorMode: true,
}

const Theme = extendTheme({
    colors: {
        brand: {
            100: "#323130",
            200: "#f5f6f7",
            900: "#dff4ce",
        },
    },
    config
})

export default Theme