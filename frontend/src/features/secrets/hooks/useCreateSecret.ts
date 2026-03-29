import { useMutation } from "@tanstack/react-query";

export default function useCreateSecret() {
    return useMutation({
        mutationFn: ({ expiryDays, maxViews, password }: { expiryDays: number; maxViews: number; password: string }) =>
            fetch("/api/secrettexts", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ expiryDays, maxViews, password }),
            }),
    })
}
