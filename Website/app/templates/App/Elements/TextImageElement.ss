<div class="section section--textimage $Highlight $Variant $ImgWidth">
    <% if $Image %>
        <div class="textimage_image">
            $Image.ScaleWidth(800)
        </div>
    <% end_if %>

    <div class="textimage_text">
        <div class="textimage_text_content">
            <h2 class="textimage_text_title">$Title</h2>
            $Text
        </div>
    </div>
</div>
