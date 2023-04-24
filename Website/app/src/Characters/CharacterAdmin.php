<?php
namespace App\CharacterDatabase;

use SilverStripe\Admin\ModelAdmin;
use App\CharacterDatabase\CharacterPart;

/**
 * Class \App\Database\ExperienceAdmin
 *
 */
class CharacterAdmin extends ModelAdmin
{
    private static $managed_models = array (
        CharacterPart::class,
    );

    private static $url_segment = "character-parts";

    private static $menu_title = "Character Parts";

    private static $menu_icon = "app/client/icons/block-text.svg";
}
