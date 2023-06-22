import { AuthOptions } from "next-auth";
import NextAuth from "next-auth/next";
import { OAuthConfig } from "next-auth/providers";



const provider: OAuthConfig<any> = {
    id: "aceicar",
    version:"2.0",
    name: "aceicar",
    clientId: "AceicarWebClient",
    clientSecret: "AceicarProviderSecret",
    type: "oauth",
    checks: ["pkce", "state"],
    idToken: true,
    issuer: "https://localhost:5000",
    wellKnown: "https://localhost:5000/.well-known/openid-configuration",
    client: {
        authorization_signed_response_alg: 'HS256',
    },
    authorization: {
        url: "https://localhost:5000/oauth/authorize",
        params: {
            scope: 'openid api',
        },
    },
    token: "https://localhost:5000/oauth/token",
    profile(profile, token) {
        console.log(profile)
        console.log(token)
        return profile;
    },

}

const OPTIONS: AuthOptions = {
    secret:"RANDOM",
    providers: [
        provider

    ],
    callbacks: {
        signIn(params) {
            console.log(params)
            return true;
        }
    }
}
const handler = NextAuth(OPTIONS)
export {handler as GET,handler as POST}