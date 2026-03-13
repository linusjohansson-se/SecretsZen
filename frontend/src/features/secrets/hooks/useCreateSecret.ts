import { useMutation } from "@tanstack/react-query";

export default function useCreateSecret() {
    return useMutation({
        mutationFn: ({ expiryDays, maxViews, password }) =>
            fetch("/api/secrets", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ expiryDays, maxViews, password }),
            }),
    })
}
